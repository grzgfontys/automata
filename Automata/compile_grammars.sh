#!/usr/bin/env bash

script_dir=$(dirname -- "$0")
cd "$script_dir" || exit;

commit=1
if [[ "$*" =~ "-no-commit" ]]; then
  commit=0
fi;

# shellcheck disable=SC2044
for file in $(find "./Grammar" -type f -name "*.g4"); do
  file_name=${file##*/Grammar/}
  read -r namespace < <(echo "Grammar/${file_name%/*.g4}" | tr "/" ".")
  echo "Compiling $file_name"
  antlr -visitor -package "$namespace" -Dlanguage=CSharp "$file"
done

if [[ $commit -eq 1 ]]; then
  # unstage currently staged files to only commit generated files
  staged_files=()
  for file in $(git status -s | grep "^\w.*$" | cut -b4-); do
    staged_files+=("$file")
    git restore --staged "$file"
  done;
  
  generated_grammars=()
  # add all of the generated .cs files
  find "./Grammar" -type f -name "*.cs" -exec git add {} \;
  
  # shellcheck disable=SC2207
  modified_files=( $(git status -s | grep "^\w.*$" | cut -b4-) )
  for file in "${modified_files[@]}"; do
    echo "File: $file"
    # remove all the suffixes
    file="${file%.cs}"
    file="${file%BaseListener}"
    file="${file%Listener}"
    file="${file%BaseVisitor}"
    file="${file%Visitor}"
    file="${file%Parser}"
    file="${file%Lexer}"
    file+=".g4"
    
    # if the grammar is not 
    if [[ ! "${generated_grammars[*]}" =~ ${file} ]]; then
      if [[ ! -f "$file" ]]; then
        >&2 echo "ERROR: File $file not found but has generated files"
        exit
      fi;
      generated_grammars+=("$file")
    fi;
  done;
  
  grammar_list=$(printf " %s\n" "${generated_grammars[@]}")
  commit_message=$(printf "Generated files for the following grammars:\n%s" "$grammar_list")

  if [[ ${#modified_files[@]} -gt 0 ]]; then
    git commit -m "$commit_message"
  else
    echo "No files changed"
  fi;
  
  # restore previously staged files
  for file in "${staged_files[@]}"; do
    git add "$file"
  done;
  
fi;