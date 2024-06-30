all: rsa.pdf

rsa.pdf: rsa.tex
  xelatex rsa.tex && xelatex rsa.tex

clean:
  rm -rf rsa.toc rsa.aux rsa.pdf texput.log rsa.out 2> /dev/null || true
