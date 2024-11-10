set encoding utf8
set terminal pdfcairo enhanced color dashed font "IBM Plex Mono, 14" rounded size 19 cm, 12 cm

set title "Miller Rabin Fehlerquotient nach Iterationen"
set key off
set output 'mr_error.pdf'

set xlabel "Iteration"
set ylabel "Fehlerquotient"
set autoscale x
set yrange [2.5e-65:0.25]
set grid
set autoscale y
set ytics (2.5e-65, 2.5e-2, 0.25)
set xtics 0,5,100
set tics out
set key right top

set xrange [1:100]

plot "./col" using 1:2 with line lc "#4a2387" lw 2 title "(1/4)^i"
