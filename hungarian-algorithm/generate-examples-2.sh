#!/bin/bash

set -euxo pipefail

for i in 7500 12500 15000 17500
do
  for j in {1..10}
  do
    mkdir -p examples/$j
    ./publish/example-generator $i --rand-min 1 --rand-max 5 -p 0.9 --output examples/$j/$i.txt
  done
done
