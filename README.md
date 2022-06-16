
# OnlineShop

OnlineShop is a small exercise to show microservice architecture, how microservices communicate with each other, how and when use a queue mechanism etc. As you see on the image I created four microservices. Each service has own database except RabbitMqService. RabbitMqService has no database. I used postgresql as database. Also I used Fluent migrations framework for creating DB and tables. There is no code relation between each other. The Microservices talk to each other over HTTPClient. I followed CORS pattern but as you know for CORS we need many extra scenarios and big models. I think it is enough you can see how to apply the pattern. Also I wrote some test codes. As you know writing test code is more expensive than real coding. So I could't put test codes for everywhere layer. I hope you like it.

![alt text](https://github.com/ErdoganMutlu/OnlineShop/blob/main/Architecture.jpg?raw=true)

# How to Run
- Install docker on your machine
- open powershell and run docker-compose up
- I coded on mac it is working on windows as well.


## ProductService
http://localhost:5050/swagger/index.html

- POST a Product
- You will get ProductId as response

## CustomerService
http://localhost:5051/swagger/index.html

- POST a Customer
- You will get CustomerId as response


## OrderService
http://localhost:5052/swagger/index.html

- POST a few Orders use ProductId and CustomerId before you saved
- GET Orders by filter datetime
- For example from = 2022-06-15 20:24:13  to = 2022-06-16 20:24:13



## RabbitMqService
http://localhost:5053/swagger/index.html
