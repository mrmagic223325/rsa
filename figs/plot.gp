set encoding utf8
set terminal pdfcairo enhanced color dashed font "IBM Plex Mono, 14" rounded size 16 cm, 9.6 cm

set style line 1 lc rgb '#FF6961' pt 1 ps 1 lt 1 lw 2 # red
set style line 2 lc rgb '#A1B8FB' pt 6 ps 1 lt 1 lw 2 # blue
set style line 3 lc rgb '#E0F9CF' pt 2 ps 1 lt 1 lw 2 # green
set style line 4 lc rgb '#54487A' pt 3 ps 1 lt 1 lw 2 # purple
set style line 5 lc rgb '#F2DEDE' pt 4 ps 1 lt 1 lw 2 # orange
set style line 6 lc rgb '#FDFD96' pt 5 ps 1 lt 1 lw 2 # yellow
set style line 7 lc rgb '#A4D8D8' pt 7 ps 1 lt 1 lw 2 # brown
set style line 8 lc rgb '#F49AC2' pt 8 ps 1 lt 1 lw 2 # pink
set palette maxcolors 8
set palette defined ( 0 '#FF6961', 1 '#A1B8FB', 2 '#E0F9CF', 3 '#54487A',\
4 '#F2DEDE', 5 '#FDFD96', 6 '#A4D8D8', 7 '#F49AC2' )

# Standard border
set style line 11 lc rgb '#777777' lt 1 lw 3
set border 0 back ls 11
set tics out nomirror

# Standard grid
set style line 12 lc rgb '#777777' lt 0 lw 1
set grid back ls 12
unset grid

set xlabel "x"
set ylabel "f(x)"
set grid
set key right top
set xrange[0:6.28]
set yrange[-1:1]
set output 'sinx.pdf'

plot sin(x) w l ls 1, cos(x) w l ls 2
