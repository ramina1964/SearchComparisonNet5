# SearchComparisonNet5
This is WPF desktop application using .NET 5.0. It compares a Linear vs a Binarch search for finding a random integer in an array. The results are averages of No.
of iterations and elapsed time for both search methods. The input parameters are no. of searches and the array length.

Before simulation can start we have to initialize the array with random numbers. In addition, we sort the array to be able to use binary search. In case of Linear Search we
go through the array element by element to locate the value. For the Binary Approach half of the remaining array is thrown away in every iteration. A progressbar is also
implemented to show the percentage of finished work.

Finally, the input parameters are checked. The requirements, in addition to be of the correct type, are that they must be a predefined range. 
