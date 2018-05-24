.PHONY: clean

default:
	./latexrun --latex-args='-shell-escape' master.tex

clean:
	rm -f master.out master.log master.aux

cleanall: clean
	#rm -f master.pdf
	rm -rf _minted-master/
	rm -rf latex.out/
