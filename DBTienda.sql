create database DBTienda
go
-- Usar la Base de datos Tienda
USE DBTienda
go
-- Categoria
create table Categoria(
IdCategoria int primary key identity,
Nombre varchar(50)
)
go
-- Producto
create table Producto(
IdProducto int primary key identity,
Nombre varchar(50),
Precio float,
FechaCreacion datetime,
Foto varchar(200),
FKCategoria int,
foreign key(FKCategoria) references Categoria)
go
-- Insert una Categoria
insert into categoria(Nombre) values
('Aves y Cárnicos'),
('Comestible/Abarrotes'),
('Frutas')
go
-- Insert un Producto
insert into Producto(Nombre,Precio,FechaCreacion,FKCategoria) values 
('Aceite',20.64,getdate(),2),
('Atůn Nair en Aceite',11.91,getdate(),2),
('Azucar',18.12,getdate(),2),
('Bolillo',1.47,getdate(),2),
('Café de Grano 908 Gr',148.85,getdate(),2),
('Cafe Soluble 95 Gr',42.97,getdate(),2),
('Cajeta Coronado 550 Gr',56.08,getdate(),2),
('Chiles Jalapeńos 220 Gr',7.57,getdate(),2),
('Chocolate Choco Milk 800 Gr',57.28,getdate(),2),
('Galletas Maria 850 Gr',34.80,getdate(),2),
('Aguacate Hass 1Kg',31.48,getdate(),3),
('Guayaba 1Kg',20.97,getdate(),3),
('Jicama 1Kg',13.33,getdate(),3),
('Limón con semilla 1Kg',36.07,getdate(),3),
('Manzana Amarilla 1Kg',32.84,getdate(),3),
('Manzana Roja Starking 1Kg',36.68,getdate(),3),
('Naranja 1Kg',8.30,getdate(),3),
('Papaya 1Kg',20.24,getdate(),3),
('Plátano Tabasco 1Kg',15,getdate(),3),
('Toronja 1Kg',10.46,getdate(),3)

go
-- Ventas
create table Venta(
IdVenta int primary key identity,
DiaVenta datetime,
Subtotal float,
Iva float,
Total float)
go
-- Lista Ventas
create table ListaVenta(
IdListaVenta int primary key identity,
FKVenta int,
FKProducto int,
Cantidad int,
Total float,
foreign key(FKVenta) references Venta,
foreign key(FKProducto) references Producto
)

