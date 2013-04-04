//Alex Mitenev 2013

let mutable (primes : int64[]) = Array.empty

let prime (n : int64) =
  let mutable primes = [|2L|]
  let mutable j = 3L
  let mutable i = 0
  while int64(primes.[Array.length primes - 1]) < n && j < n do
    i <- 0
    while j % primes.[i] <> 0L && i <> Array.length primes - 1 do
      i <- i + 1
    if i = Array.length primes - 1 then 
      primes <- Array.append primes [|j|]
    j <- j + 1L
  primes  

let sum num =
  let mutable s : int64 = 0L
  let primes = prime num
  let sumArray primes = Array.fold (fun (acc : int64) (elem : int64) -> acc + elem) 0L primes
  sumArray primes

printf "%A" <| sum 2000000L