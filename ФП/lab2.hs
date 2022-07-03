fact :: Int -> Int
fact 0 = 1
fact n = n * fact(n - 1) 

tr :: Int -> Int
tr 1 = 1
tr n = n + tr(n - 1)

pir :: Int -> Int
pir 1 = 1
pir n = pir(n-1) + tr(n)

task1 :: Int -> [Int]
task1 0 = []
task1 n = task1(n - 1)++[n]

task2 :: Int -> [Int]
task2 n = [2*i+1|i<-[0..n-1]]

task3 :: Int -> [Int]
task3 n = [2*i|i<-[1..n]]

task4 :: Int -> [Int]
task4 n = [i*i*i|i<-[1..n]]

task5 :: Int -> [Int]
task5 n = [fact i|i<-[1..n]]

task6 :: Int -> [Int]
task6 n = [10^i|i<-[1..n]]

task7 :: Int -> [Int]
task7 n = [tr i|i<-[1..n]]

task8 :: Int -> [Int]
task8 n = [pir i|i<-[1..n]]

task9 :: Int -> Int
task9 1 = 1
task9 n = n*n + task9(n - 1)

task10 :: Int -> Int
task10 1 = 1
task10 n = n*task10(n - 1)

f::Char->Char->String->String 
f simIn simOut str = [if i == simIn then simOut else i|i<-str]