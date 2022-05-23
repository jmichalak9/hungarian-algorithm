#!/bin/bash

set -euxo pipefail

for i in 10 20 50 100 200 500 1000 2000 5000 10000 20000
do
   ./publish/example-generator $i --rand-min 1 --rand-max 5 --output examples/$i.txt
done
