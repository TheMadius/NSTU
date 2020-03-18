import math 
import matplotlib.pyplot as plt
import numpy as np

global PI 
PI = 3.14159265359

def pattern():
    lY =[]
    lX =[]
    
    k = (2 * PI)/lamda
    npoint = 20000
    dy = ymax/npoint
    delta = a/float(nslit-1)
    phase = (2 * PI)/float(nbar)
    L2= L*L

    Lnpoint = list(range(-npoint,npoint+1))

    for ipoint in Lnpoint:
        y = ipoint*dy
        intensity = 0.0
        Lnbar = list(range(1,nbar+1))
        for t in Lnbar:
            amplitude = 0.0
            Lnslit = list(range(1,nslit+1))
            for islit in Lnslit:
                yslit = -0.5*a + (islit - 1) *delta
                yslit = yslit - y
                r2 = L2 + yslit*yslit
                r = math.sqrt(r2)
                amplitude = (1/r)*math.cos(k*r - phase*float(t)) + amplitude
            intensity = intensity + amplitude*amplitude
        intensity = float(intensity)/float(nbar)
        lY.append(intensity)
        lX.append(y)
        #plt.plot([y],[intensity],marker='.', color='r',linewidth = 0.01)
        #plt.plot([y],[intensity],linewidth = 1)
    plt.plot(lX,lY,linewidth = 1)

    
def initial():
    
    global nslit 
    global a 
    global L 
    global lamda 
    global ymax 
    global nbar
      
    #nslit = int(input("количество щелей = "))
    #a = float(input("расстояние между щелями(мм) = "))
    #L = float(input("расстояние до экрана(мм) = "))
    #lamda = float(input("длина волны(ангстремы) = "))
    #ymax = float(input("макс. координата на фотопластине(мм) = "))
    #nbar = int(input("число точек усреднения интенсивности = "))
      
    nslit = int(2)
    a = float(0.1)
    L = float(200)
    lamda = float(500)
    ymax = float(5)
    nbar = int(3)

    lamda = lamda* (10**(-7))

    lmax =  (nslit/L)**2
    
    x = np.linspace(0,lmax,100);

    zero = np.zeros(100)

    lineG = np.linspace(-ymax,ymax,100);

    plt.plot(lineG,zero,color = 'red')

    plt.plot(zero,x,color = 'blue')

    del(x)
    del(lineG)

    dy = lamda*L/a

    ntick = int(ymax/dy)

    Lt = 0.01*lmax
    
    Lntick = list(range(-ntick,ntick+1))

    x = np.linspace(0,Lt,100)

    for i in Lntick:
           y = zero +i*dy 
           plt.plot(y,x,color = 'red',linewidth=1)

    return

initial()

pattern()

plt.show()
