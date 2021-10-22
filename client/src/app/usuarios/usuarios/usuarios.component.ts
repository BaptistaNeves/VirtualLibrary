import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Usuario } from 'src/app/_models/produto/usuario';
import { UsuarioService } from 'src/app/_services/produto/usuario.service';

@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.css']
})
export class UsuariosComponent implements OnInit {
  adicionarForm: FormGroup;
  editarForm: FormGroup;
  editarConfForm: FormGroup;
  usuarios: any;
  usuario: any;
  roles: any;
  modelStateErrors: any;

  constructor(private usuarioService: UsuarioService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarUsuarios();
    this.listarRoles();
    this.initializeAddForm();
    this.initializeEditForm();
    this.initializeEditConfigForm();
  }

  initializeAddForm() {
    this.adicionarForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['',[Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.maxLength(9), Validators.minLength(9)]],
      endereco: ['', [Validators.required, Validators.minLength(5)]],
      role: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  initializeEditForm() {
    this.editarForm = this.formBuilder.group({
      userName: ['', [Validators.required, Validators.minLength(3)]],
      phoneNumber: ['', [Validators.required, Validators.maxLength(9), Validators.minLength(9)]],
      endereco: ['', [Validators.required, Validators.minLength(5)]],
      role: ['', Validators.required]
    });
  }

  initializeEditConfigForm() {
    this.editarConfForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      currentPassword: ['', [Validators.required, Validators.minLength(6)]]
    });
  }


  listarUsuarios() {
    this.usuarioService.obterTodos().subscribe(response => {
      this.usuarios = response;
      console.log(this.usuarios);
    })
  }

  listarRoles() {
    this.usuarioService.obterRoles().subscribe(response => {
      this.roles = response;
    })
  }

  limparCampos() {
    this.adicionarForm.reset();
  }

  limparSenhas() {
    this.editarConfForm.reset();
  }

  get f() {return this.adicionarForm.controls}
  get e() {return this.editarForm.controls}
  get ec() {return this.editarConfForm.controls}

  editar(usuario: Usuario) {
    this.usuario = usuario;
    this.editarForm.controls['userName'].setValue(usuario.userName);
    this.editarForm.controls['endereco'].setValue(usuario.endereco);
    this.editarForm.controls['phoneNumber'].setValue(usuario.phoneNumber);
    this.editarConfForm.controls['email'].setValue(usuario.email);
    console.log(usuario);
  }

  atualizarGeral() {
    this.usuarioService.atualizar(this.usuario.id, this.editarForm.value).subscribe(response => {
      let fechar: HTMLElement = document.getElementById('editGeral_fechar') as HTMLElement;
      fechar.click();
      this.listarUsuarios();
      this.toastr.success("Úsuario Editado com sucesso!");
    }, error => {
      this.modelStateErrors = error;
    })
  }

  actualizarConfig() {
    this.usuarioService.atualizarConfig(this.usuario.id, this.editarConfForm.value).subscribe(response => {
      let fechar: HTMLElement = document.getElementById('editConf_fechar') as HTMLElement;
      fechar.click();
      this.listarUsuarios();
      this.toastr.success("Alterações realizadas com sucesso!");
      this.limparSenhas();
    },error => {
      this.modelStateErrors = error;
    })
  }

  detalhes(usuario: any) {
    this.usuario = usuario;
  }

  confirmaEliminar(usuario: any) {
    this.usuario = usuario;
  }

  eliminar() {
    this.usuarioService.remover(this.usuario.id).subscribe(response => {
      let fechar: HTMLElement = document.getElementById('eliminar_fechar') as HTMLElement;
          fechar.click();
          this.listarUsuarios();
          this.toastr.success("Úsuario Removido com sucesso!");
    })
  }

  adicionar() {
    this.usuarioService.adicionar(this.adicionarForm.value).subscribe(response => {
      let fechar: HTMLElement = document.getElementById('add_fechar') as HTMLElement;
          fechar.click();
          this.listarUsuarios();
          this.toastr.success("Úsuario adicionado com sucesso!");
          this.limparCampos();
    }, error => {
      this.modelStateErrors = error;
    })
  }

}
