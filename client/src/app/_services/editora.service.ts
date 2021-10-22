import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Editora } from '../_models/editora';

@Injectable({
  providedIn: 'root'
})
export class EditoraService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
    return this.http.get<Editora>(this.baseUrl + "editoras");
  }

  obterPorId(id:string) {
    return this.http.get<Editora>(this.baseUrl + "editoras/" + id);
  }

  adicionar(editora: Editora) {
    return this.http.post<Editora>(this.baseUrl + "editoras", editora);
  }

  atualizar(id:string, editora:Editora) {
    return this.http.put<Editora>(this.baseUrl + "editoras/" + id, editora);
  }

  remover(id:string) {
    return this.http.delete(this.baseUrl + "editoras/" + id);
  }
}
