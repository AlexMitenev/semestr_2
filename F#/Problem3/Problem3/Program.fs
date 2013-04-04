//Alex Mitenev 2013

let prime (n : int64) =
  let mutable primes = [|2L|]
  let mutable j = 3L
  let mutable i = 0
  while int64(primes.[Array.length primes - 1]) < n do
    i <- 0
    while j % primes.[i] <> 0L && i <> Array.length primes - 1 do
      i <- i + 1
    if i = Array.length primes - 1 then 
      primes <- Array.append primes [|j|]
    j <- j + 1L
  primes


let primeGCD (num : int64) =
  let sqrtNum = int64(sqrt(float num)) 
  let primes = prime sqrtNum
  let mutable i = Array.length primes - 2
  while num % primes.[i] <> 0L do 
    i <- i - 1
  primes.[i]

printfn "%A" <| primeGCD 600851475143L  

