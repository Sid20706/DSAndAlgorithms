I spent the entire day thinking about this problem, reading blogs, and finally figured this out (hopefully the feeling is true). Here are some thoughts and summary.

What determines the amount of water can a bar can hold? It is the min height of the max heights along all paths to the boundary (not just 4 direction!!!, which was my first intuition) Look at the example below. If we add 2 units of water into the 1 in the center, it will overflow to 0.
0 0 3 0 0
0 0 2 0 0
3 2 1 2 3
0 0 2 0 0
0 0 3 0 0

Just like 1-D two pointer approach, we need to find some boundary. Because all boundary cells cannot hold any water for sure, we use them as the initial boundary naturally.

Then which bar to start? Find the min bar (let's call it A) on the boundary (heap is a natural choice), then do 1 BFS (4 directions). Why BFS? Because we are sure that the amount of water that A's neighbors can hold is only determined by A now for the same reason in 1D two-pointer approach.

How to update the heap during BFS
Step 1. Remove the min bar A from the heap
Step 2. If A's neighbor B's height is higher, it cannot hold any water. Add it to the heap
Step 3. If B's height is lower, it can hold water and the amount of water should be height_A - height_B. Here comes the tricky part, we still add B's coordinate into the heap, BUT change its height to A's height because A is the max value along this path (for this reason we cannot just use heightMap and need a class/array to store it's coordinates and UPDATED height). And we can think of B as a replacement of A now and never worry about A again. Therefore a new boundary is formed and we can repeat this process again.
