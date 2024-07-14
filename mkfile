all: rsa.pdf

rsa.pdf: rsa.tex chapters/examples/examples.tex chapters/principles/principles.tex chapters/conclusion.tex chapters/examples/prelim.tex chapters/introduction.tex
  xelatex -shell-escape rsa.tex && xelatex -shell-escape rsa.tex

clean:
  rm -rf rsa.toc rsa.aux rsa.pdf texput.log rsa.out 2> /dev/null || true
