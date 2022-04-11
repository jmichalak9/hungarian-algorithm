#!/bin/bash

set -euxo pipefail

for i in 10 20 50 100 200 500 1000 2000 5000
do
   ./publish/example-generator -- $i -p 1 examples/$i.txt
done
