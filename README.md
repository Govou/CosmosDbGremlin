# CosmosDbGremlin

Problem statement:

This involves movement of Airlines movement.
They basically consist of a vast number of travelling destination points that are connected to each other in a linear way. 
By analyzing the connections between them, we can infer flight routes from one destination to another, optimize flight routes and make best routes suggestions to passengers.
These relationship could get more complex as the distination paths grow.



Assumptions: 

Flight movement from one destination to another is unidirectional.
More destination routes may be added in the future.
The Ariplane can only move from from destination to another at a time.
The Airplane takes off from the starting point



Solution approach:

The best approach I took  was processing this information as a graph-oriented data using Azure Cosmos DB. 
A graph allows us to represent connections between entities in a convenient and natural way. 
In many data processing scenarios, we want to efficiently store and analyze huge amounts of these connections.

Azure Cosmos DB was used as it enabled me to efficiently store and analyze highly connected data using graph structures.
Cosmos DB also supports seamless scaling and multi-region replication of graphs, it is well-suited for large-scale data processing scenarios.

Graphs were read and manipulated using Gremlin, a popular graph traversal language originated from the Apache TinkerPop project.

Gremlin queries was used to construct a graph and store it inside Cosmos DB and also manipulate data.


