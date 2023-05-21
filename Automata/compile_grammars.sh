#!/usr/bin/env bash

script_dir=$(dirname -- "$0")

# shellcheck disable=SC2044
for file in $(find "$script_dir/Grammar" -type f -name "*.g4"); do
  file_name=${file##*/Grammar/}
  read -r namespace < <(echo "Grammar/${file_name%/*.g4}" | tr "/" ".")
  echo "Compiling $file_name"
  antlr -package "$namespace" -Dlanguage=CSharp "$file"
done

find "$script_dir/Grammar" -type f -name "*.cs" -exec git add {} \;
