import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Fornecedor } from '../_models/fornecedor';

@Injectable({
  providedIn: 'root'
})
export class FornecedorService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
    return this.http.get<Fornecedor>(this.baseUrl + "fornecedores");
  }

  obterPorId(id:string) {
    return this.http.get<Fornecedor>(this.baseUrl + "fornecedores/" + id);
  }

  adicionar(fornecedor: Fornecedor) {
    return this.http.post<Fornecedor>(this.baseUrl + "fornecedores", fornecedor);
  }

  atualizar(id:string, fornecedor:Fornecedor) {
    return this.http.put<Fornecedor>(this.baseUrl + "fornecedores/" + id, fornecedor);
  }

  remover(id:string) {
    return this.http.delete(this.baseUrl + "fornecedores/" + id);
  }
}
