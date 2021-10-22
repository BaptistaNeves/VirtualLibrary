import { Usuario } from "./produto/usuario";

export interface Editora {
    id:string;
    nome:string;
    cidade:string;
    pais:string;
    descricao:string;
    dataCadastro:Date;
    dataAtualizacao:Date;
    usuario:Usuario;
}