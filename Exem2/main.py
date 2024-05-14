import matplotlib.pyplot as plt
import numpy as np
import pylab

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
    lst_tmp.pop(2)
    lst_tmp[-1] = lst_tmp[-1][:-1]
    h = float(lst_tmp[1]) - float(lst_tmp[0])
    xi.append(float(lst_tmp[2]))
    ni.append(float(lst_tmp[3]))
    ni_division_h.append(float(lst_tmp[3]))
    wi.append(float(lst_tmp[4]))
    wi_division_h.append(float(lst_tmp[5]))

pylab.subplot(221)
pylab.bar(xi,ni_division_h, width=h,edgecolor='black', color="violet" )
pylab.title ("Гистограмма частот")

pylab.subplot(222)
pylab.bar(xi, wi_division_h, width=h, edgecolor='black', color="violet")
pylab.title ("Гистограмма относительных частот")

pylab.subplot(223)
pylab.plot(xi, ni, color='orange')
pylab.title ("Полигон частот")

pylab.subplot(224)
pylab.plot(xi, wi, color='green')
pylab.title ("Полигон относительных частот")

pylab.show()

'''import os
import eel 
try:
    eel.init(f'{os.path.dirname(os.path.realpath(__file__))}/web')
    eel.start("index.html",size=(1250, 850))
except Exception as e:
    print(e)'''