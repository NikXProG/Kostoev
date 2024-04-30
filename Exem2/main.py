'''import matplotlib.pyplot as plt
import numpy as np
import pylab
print(plt.get_backend())
f = open('./data.txt', 'r')
f.readline();
xi = [];
wi = [];
ni = [];
wi_division_h = [];
ni_division_h = [];
for line_file in f:
    lst_tmp = line_file.split(" ")
    lst_tmp.pop(0)
    lst_tmp.pop(0)
    lst_tmp[-1] = lst_tmp[-1][:-1]
    xi.append(float(lst_tmp[0]))
    ni.append(float(lst_tmp[1]))
    ni_division_h.append(float(lst_tmp[2]))
    wi.append(float(lst_tmp[3]))
    wi_division_h.append(float(lst_tmp[4]))

print(ni)
print(wi)
pylab.subplot(221)
pylab.bar(xi,ni_division_h, width=0.96,edgecolor='black', color="violet" )
pylab.title ("Гистограмма частот")

pylab.subplot(222)
pylab.bar(xi, wi_division_h, width=0.96, edgecolor='black', color="violet")
pylab.title ("Гистограмма относительных частот")

pylab.subplot(223)
pylab.plot(xi, ni, color='orange')
pylab.title ("Полигон частот")

pylab.subplot(224)
pylab.plot(xi, wi, color='green')
pylab.title ("Полигон относительных частот")

pylab.show()'''
import os
import eel 
try:
    eel.init(f'{os.path.dirname(os.path.realpath(__file__))}/web')
    eel.start("index.html",size=(1250, 850))
except Exception as e:
    print(e)
finally:
    print("\nКонец программы.")
