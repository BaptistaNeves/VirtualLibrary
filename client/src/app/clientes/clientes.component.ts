import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Cliente } from '../_models/cliente';
import { ClienteService } from '../_services/cliente.service';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrls: ['./clientes.component.css']
})
export class ClientesComponent implements OnInit {
  modelStateErrors: any;
  clienteForm: FormGroup;
  clientes: any
  cliente: any;
  btnAdicionar: boolean;
  btnAtualizar: boolean;

  constructor(private clienteService: ClienteService,
              private formBuilder: FormBuilder,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.listarFornecedores();
    this.initializeForm();
  }

  listarFornecedores() {
    this.clienteService.obterTodos().subscribe(response => {
      this.clientes = response;
    })
  }

  initializeForm() {
    this.clienteForm = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      telefone: ['', [Validators.required, Validators.minLength(9), Validators.maxLength(9)]],
      endereco: ['', Validators.required],
      tipoDocumento: ['', Validators.required],
      numeroDocumento: ['', [Validators.required, Validators.minLength(13), Validators.maxLength(13)]],
      dataNascimento: ['', Validators.required]
    });
  }

  limparCampos() {
    this.clienteForm.reset();
  }

  preencherCampos(cliente: Cliente) {
    this.clienteForm.controls['nome'].setValue(cliente.nome);
    this.clienteForm.controls['email'].setValue(cliente.email);
    this.clienteForm.controls['telefone'].setValue(cliente.telefone);
    this.clienteForm.controls['endereco'].setValue(cliente.endereco);
    this.clienteForm.controls['tipoDocumento'].setValue(cliente.tipoDocumento);
    this.clienteForm.controls['numeroDocumento'].setValue(cliente.numeroDocumento);
    this.clienteForm.controls['dataNascimento'].setValue(cliente.dataNascimento);
  }

  fecharModal(referencia:string) {
    let btn_fechar: HTMLElement = document.getElementById(referencia) as HTMLElement;
    btn_fechar.click();
  }

  clickAdicionar() {
    this.modelStateErrors = null;
    this.btnAdicionar = true;
    this.btnAtualizar = false;
    this.limparCampos();
  }

  clickRemover(cliente: Cliente) {
    this.modelStateErrors = null;
    this.cliente = cliente;
  }

  clickDetalhes(cliente: Cliente) {
    this.cliente = cliente;
  }

  clickAtualizar(cliente: Cliente) {
    this.modelStateErrors = null;
    this.btnAtualizar = true;
    this.btnAdicionar = false;
    this.cliente = cliente;
    this.preencherCampos(cliente);
  }

  get f(){return this.clienteForm.controls;}

  adicionar() {
    this.clienteService.adicionar(this.clienteForm.value).subscribe(() => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.toastr.success("Cliente Adicionado com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  atualizar() {
    this.clienteService.atualizar(this.cliente.id, this.clienteForm.value).subscribe(() => {
      this.fecharModal('modal_fechar');
      this.limparCampos();
      this.toastr.success("Cliente Atualizado com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }

  remover() {
    this.clienteService.remover(this.cliente.id).subscribe(() => {
      this.fecharModal('eliminar_fechar');
      this.toastr.success("Cliente Removido com Sucesso!");
      this.listarFornecedores();
    }, error => {
      this.modelStateErrors = error;
    })
  }
}

