import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Cliente } from '../_models/cliente';

@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
    return this.http.get<Cliente>(this.baseUrl + "clientes");
  }

  obterPorId(id:string) {
    return this.http.get<Cliente>(this.baseUrl + "clientes/" + id);
  }

  adicionar(cliente: Cliente) {
    return this.http.post<Cliente>(this.baseUrl + "clientes", cliente);
  }

  atualizar(id:string, cliente:Cliente) {
    return this.http.put<Cliente>(this.baseUrl + "clientes/" + id, cliente);
  }

  remover(id:string) {
    return this.http.delete(this.baseUrl + "clientes/" + id);
  }
}
