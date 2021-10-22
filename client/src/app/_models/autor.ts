import { Usuario } from "./produto/usuario";

export interface Autor {
    id: string;
    nome: string;
    nacionalidade: string;
    descricao: string;
    dataCadastro: Date;
    dataAtualizacao: Date;
    usuario: Usuario;
}