import { Component, OnInit } from '@angular/core';
import { ProcessInfo, ProcessService } from '../../services/process.service';

@Component({
  selector: 'app-process-list',
  standalone: true,
  imports: [],
  templateUrl: './process-list.component.html',
  styleUrl: './process-list.component.css'
})
export class ProcessListComponent implements OnInit {
  public processes: ProcessInfo[] = [];
  constructor(private processService: ProcessService) { }

  ngOnInit(): void {
    this.processService.getProcesses()
      .subscribe(procs => { this.processes = procs });
  }
}
