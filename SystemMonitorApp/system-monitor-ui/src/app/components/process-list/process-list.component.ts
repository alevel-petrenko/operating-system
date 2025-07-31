import { Component } from '@angular/core';
import { ProcessInfo, ProcessService } from '../../services/process.service';
import { ProcessCardComponent } from '../process-card/process-card.component';
import { ComputerInfo, ComputerService } from '../../services/computer.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-process-list',
  standalone: true,
  imports: [
    ProcessCardComponent,
    AsyncPipe
  ],
  templateUrl: './process-list.component.html',
  styleUrl: './process-list.component.css'
})
export class ProcessListComponent {
  public processes$: Observable<ProcessInfo[]>;
  public computerName$: Observable<ComputerInfo>;

  constructor(
    private processService: ProcessService,
    private computerService: ComputerService
  ) {
    this.processes$ = this.processService.getProcesses();
    this.computerName$ = this.computerService.getComputerName();
  }
}
