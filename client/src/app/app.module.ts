import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { NgxSpinnerModule } from "ngx-spinner";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout.component';
import { InicioComponent } from './inicio/inicio.component';
import { UsuariosComponent } from './usuarios/usuarios/usuarios.component';
import { PerfilComponent } from './perfil/perfil.component';
import { CategoriasComponent } from './categoria/lista-categoria/categorias.component';
import { ReactiveFormsModule } from '@angular/forms';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { LoginComponent } from './login/login.component';
import { TokenInterceptor } from './_interceptors/token.interceptor';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { AutoresComponent } from './autores/autores.component';
import { EditorasComponent } from './editoras/editoras.component';
import { FornecedoresComponent } from './fornecedores/fornecedores.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ClientesComponent } from './clientes/clientes.component';
import { LivrosComponent } from './livros/livros.component';
import { LivroDetalhesComponent } from './livro-detalhes/livro-detalhes.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    InicioComponent,
    UsuariosComponent,
    PerfilComponent,
    CategoriasComponent,
    LoginComponent,
    AutoresComponent,
    EditorasComponent,
    FornecedoresComponent,
    ClientesComponent,
    LivrosComponent,
    LivroDetalhesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    BsDatepickerModule.forRoot()
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
