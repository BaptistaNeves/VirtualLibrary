import { Usuario } from "./usuario";

export interface Categoria {
    id: string;
    descricao: string;
    dataCadastro: Date;
    dataAtualizacao: Date;
    usuario: Usuario;
}