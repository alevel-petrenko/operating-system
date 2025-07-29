import { Component } from '@angular/core';
import { ProcessInfo, ProcessService } from '../../services/process.service';
import { ProcessCardComponent } from '../process-card/process-card.component';
import { ComputerService } from '../../services/computer.service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-process-list',
  standalone: true,
  imports: [
    ProcessCardComponent
  ],
  templateUrl: './process-list.component.html',
  styleUrl: './process-list.component.css'
})
export class ProcessListComponent {
  public processes: ProcessInfo[] = [];
  public computerName: string = '';

  constructor(private processService: ProcessService,
    private computerService: ComputerService
  ) { }

  public getProcesses(): void {
    this.processService.getProcesses()
      .subscribe(processes => this.processes = processes);
  }

  public getComputerName(): void {
    this.computerService.getComputerName()
      .subscribe(name => this.computerName = name);
  }
}
