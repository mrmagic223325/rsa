set encoding utf8
set terminal pdfcairo enhanced color dashed font "IBM Plex Mono, 14" rounded size 16 cm, 9.6 cm

set title "Endings Of Base Two Logarithms Of Powers Of Two"
set key off
set output 'endings.pdf'

set logscale y
set xlabel "Ending"
set ylabel "Power Of Two"
set autoscale
set grid
set tics out
set tics scale 1,0
set key left top

plot "./col2" using 2:1 with line lc "#4a2387" title "Endings"
