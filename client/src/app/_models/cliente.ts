import { Usuario } from "./produto/usuario";

export interface Cliente {
    id:string;
    nome:string;
    email:string;
    telefone:string;
    endereco:string;
    tipoDocumento:string;
    numeroDocumento:string;
    dataNascimento:Date;
    idade:number;
    dataCadastro:Date;
    dataAtualizacao:Date;
    usuario:Usuario;
}