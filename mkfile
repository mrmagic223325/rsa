all: rsa.pdf

rsa.pdf: rsa.tex chapters/examples.tex
  xelatex -shell-escape rsa.tex && xelatex -shell-escape rsa.tex

clean:
  rm -rf rsa.toc rsa.aux rsa.pdf texput.log rsa.out 2> /dev/null || true
