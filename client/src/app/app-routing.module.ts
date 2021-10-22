import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { AutoresComponent } from './autores/autores.component';
import { CategoriasComponent } from './categoria/lista-categoria/categorias.component';
import { ClientesComponent } from './clientes/clientes.component';
import { EditorasComponent } from './editoras/editoras.component';
import { FornecedoresComponent } from './fornecedores/fornecedores.component';
import { InicioComponent } from './inicio/inicio.component';
import { LayoutComponent } from './layout/layout.component';
import { LivroDetalhesComponent } from './livro-detalhes/livro-detalhes.component';
import { LivrosComponent } from './livros/livros.component';
import { LoginComponent } from './login/login.component';
import { PerfilComponent } from './perfil/perfil.component';
import { UsuariosComponent } from './usuarios/usuarios/usuarios.component';
import { AuthGuard } from './_helpers/auth.guard';

const routes: Routes = [
  {
    canActivate: [AuthGuard],
    path: '',
    component: LayoutComponent,
    children: [
      {path: '', component: InicioComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'usuarios', component: UsuariosComponent, data: {permittedRole: ['Admin']}, canActivate: [AuthGuard]},
      {path: 'livros', component: LivrosComponent, data: {permittedRole: ['Admin']}, canActivate: [AuthGuard]},
      {path: 'livro/:id', component: LivroDetalhesComponent, data: {permittedRole: ['Admin']}, canActivate: [AuthGuard]},
      {path: 'perfil', component: PerfilComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'categorias', component: CategoriasComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'autores', component: AutoresComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'editoras', component: EditorasComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'fornecedores', component: FornecedoresComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]},
      {path: 'clientes', component: ClientesComponent, data: {permittedRole: ['Moderador', 'Admin']}, canActivate: [AuthGuard]}
    ]
  },
  {path: 'login', component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
