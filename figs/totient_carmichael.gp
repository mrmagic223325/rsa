set encoding utf8
set terminal pdfcairo enhanced color dashed font "IBM Plex Mono, 14" rounded size 19 cm, 12 cm

set key off
set output 'totient_carmichael.pdf'

set xlabel "n"
set autoscale x
set yrange [0:1000]
set grid
set autoscale y
set xtics 0,100,1000
set tics out
set key left top

set xrange [0:1001]

plot "./totatives" using 1:2 with points pt 7 ps 0.25 lc "#FF6961" lw 2 title "φ(n)", './carmichael' using 1:2 with points pt 7 ps 0.25 lc "#4a2387" lw 2 title "λ(n)"
