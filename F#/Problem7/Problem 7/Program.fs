let prime n =
  let mutable primes = [||]
  let mutable j = 2
  let mutable i = 0
  while Array.length primes < n do
    i <- 0
    while j % primes.[i] <> 0 && i <> Array.length primes - 1 do
      i <- i + 1
    if i = Array.length primes - 1 then 
      primes <- Array.append primes [|j|]
    j <- j + 1
  primes.[n - 1]  

printf "%A" <| prime 10001


