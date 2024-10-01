CREATE DATABASE reservasdb;
GO

USE reservasdb;
GO

--- Creación de la tabla de Usuarios
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    UserPassword VARCHAR(255) NOT NULL,
    UserRole VARCHAR(100) NOT NULL
); 
GO

-- Creación de la tabla cliente
CREATE TABLE cliente (
    id_cliente INT PRIMARY KEY IDENTITY,
    UserId INT,
    nombre NVARCHAR(100) NOT NULL,
    correo NVARCHAR(100),
    telefono NVARCHAR(20),
    fecha_registro DATE DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Creación de la tabla mesas
CREATE TABLE mesas (
    id_mesa INT PRIMARY KEY IDENTITY,
    UserId INT,
    capacidad INT NOT NULL,
    ubicacion NVARCHAR(50),
    estado NVARCHAR(20) CHECK (estado IN ('disponible', 'reservada', 'ocupada')) DEFAULT 'disponible',
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Creación de la tabla reserva
CREATE TABLE reserva (
    id_reserva INT PRIMARY KEY IDENTITY,
    id_cliente INT,
    id_mesa INT,
    UserId INT,
    fecha_reserva DATE NOT NULL,
    hora_reserva TIME NOT NULL,
    num_personas INT,
    estado NVARCHAR(20) CHECK (estado IN ('pendiente', 'confirmada', 'cancelada')) DEFAULT 'pendiente',
    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente),
    FOREIGN KEY (id_mesa) REFERENCES mesas(id_mesa),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-- Insertar registros en la tabla Users
INSERT INTO Users (Username, UserPassword, UserRole)
VALUES 
('admin', 'admin123', 'Administrador'),
('juan_diaz', 'juan123', 'User');
GO

-- Insertar registros en la tabla cliente, incluyendo UserId
INSERT INTO cliente (UserId, nombre, correo, telefono)
VALUES 
(1, 'Carlos García', 'carlos.garcia@email.com', '123456789'),
(2, 'Ana Palacios', 'ana.palacios@email.com', '987654321'),
(2, 'Antonio Rodríguez', 'antonio.rodriguez@email.com', '112233445');
GO

-- Insertar registros en la tabla mesas, incluyendo UserId
INSERT INTO mesas (UserId, capacidad, ubicacion, estado)
VALUES 
(1, 4, 'Cerca de la ventana', 'disponible'),
(2, 2, 'En la terraza', 'disponible'),
(1, 6, 'Área privada', 'reservada');
GO

-- Insertar registros en la tabla reserva, incluyendo UserId
INSERT INTO reserva (id_cliente, id_mesa, UserId, fecha_reserva, hora_reserva, num_personas, estado)
VALUES 
(1, 3, 1, '2024-09-15', '20:00', 6, 'confirmada'),
(2, 1, 2, '2024-09-16', '18:30', 2, 'pendiente'),
(3, 2, 1, '2024-09-17', '19:00', 2, 'cancelada');
GO

-- Consultar todos los registros de las tablas
SELECT * FROM cliente;
SELECT * FROM mesas;
SELECT * FROM reserva;
SELECT * FROM Users;
GO
