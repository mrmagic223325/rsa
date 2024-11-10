set encoding utf8
set terminal pdfcairo enhanced color dashed font "IBM Plex Mono, 14" rounded size 16 cm, 9.6 cm

# line styles
set style line 1 lt 1 lc rgb '#B35806' # dark orange
set style line 2 lt 1 lc rgb '#E08214' # medium orange
set style line 3 lt 1 lc rgb '#FDB863' # 
set style line 4 lt 1 lc rgb '#FEE0B6' # pale orange
set style line 5 lt 1 lc rgb '#D8DAEB' # pale purple
set style line 6 lt 1 lc rgb '#B2ABD2' # 
set style line 7 lt 1 lc rgb '#8073AC' # medium purple
set style line 8 lt 1 lc rgb '#542788' # dark purple

# palette
set palette defined ( 0 '#B35806',\
    	    	      1 '#E08214',\
		      2 '#FDB863',\
		      3 '#FEE0B6',\
		      4 '#D8DAEB',\
		      5 '#B2ABD2',\
		      6 '#8073AC',\
		      7 '#542788' )

# Set plot parameters
set title "Laufzeit der CADO-NFS-Faktorisierung von 900-bit Fastprimzahlen"
set ylabel "Laufzeit in Sekunden"
set xtics 5
set x2tics 5
set grid x2
set output "factimes.pdf"
set autoscale

stats "factimes" using 1:1 nooutput
global_avg = STATS_mean_y
min = STATS_min_y
max = STATS_max_y

plot for [k=1:5] "factimes" u k smooth csplines with lines ls k title "", '' using 0:(global_avg) with lines lt 1 dt 3 t "Durchschnitt"
