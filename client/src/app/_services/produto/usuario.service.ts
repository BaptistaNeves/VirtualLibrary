import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Role } from 'src/app/_models/produto/role';
import { Usuario } from 'src/app/_models/produto/usuario';
import { UsuarioConfig } from 'src/app/_models/produto/usuarioConfig';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  obterTodos() {
    return this.http.get<Usuario>(this.baseUrl + "usuarios");
  }

  obterPorId(id: string) {
    return this.http.get<Usuario>(this.baseUrl + "usuarios/" + id);
  }

  adicionar(usuario: Usuario) {
    return this.http.post<Usuario>(this.baseUrl + "usuarios", usuario);
  }

  atualizar(id: string, usuario: Usuario) {
    return this.http.put<Usuario>(this.baseUrl + "usuarios/" + id, usuario);
  }

  atualizarConfig(id: string, model: UsuarioConfig) {
    return this.http.put<UsuarioConfig>(this.baseUrl + "usuarios/atualizar-config/" + id, model);
  }

  remover(id: string) {
    return this.http.delete<Usuario>(this.baseUrl + "usuarios/" + id);
  }

  obterRoles() {
    return this.http.get<Role>(this.baseUrl + "usuarios/obter-roles");
  }
  
}
