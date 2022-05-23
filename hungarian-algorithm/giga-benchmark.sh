#!/bin/bash

#set -euxo pipefail

for i in {1..5}
do
  echo "set=$i"
  for j in 7500 12500 15000 17500
  do
    #date
    ./publish/program examples/$i/$j.txt out.txt
  done
done
