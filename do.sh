for i in $(seq 1 1000);
do
  bc -l <<< "scale=1000;(1/4)^${i}"
done
