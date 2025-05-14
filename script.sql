create database lojabds;

use lojabds;

create table usuario (
	idUsuario int primary key auto_increment,
    nomeUsuario varchar(50),
    emailUsuario varchar(50),
    senhaUsuario varchar(50)    
);

create table produto (
	idProduto int primary key auto_increment,
    nomeProduto varchar(50),
    descricaoProduto varchar(50),
    precoProduto double,
    quantidadeProduto int
);