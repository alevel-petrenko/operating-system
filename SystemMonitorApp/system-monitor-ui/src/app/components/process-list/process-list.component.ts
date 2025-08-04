import { Component } from '@angular/core';
import { ProcessService } from '../../services/process.service';
import { ProcessCardComponent } from '../process-card/process-card.component';
import { ComputerInfo, ComputerService } from '../../services/computer.service';
import { catchError, EMPTY, map, Observable, shareReplay, tap } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { ProcessInfo } from '../../models/ProcessInfo';

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

  public currentPage = 1;
  public pageSize = 16;

  constructor(
    private processService: ProcessService,
    private computerService: ComputerService
  ) {
    this.processes$ = this.processService.getProcesses()
      .pipe(
        map(processes => processes.sort((a, b) => b.memoryUsageMb - a.memoryUsageMb)),
        tap({
          next: processes =>
          {
            processes.forEach(process => {
              process.tags = this.knownSystemNames.includes(process.name) ? ['system'] : ['user'];
            });
          }
        }),
        catchError((err) => {
          console.error('Error fetching processes:', err)
          return EMPTY;
        }),
        shareReplay({ bufferSize: 1, refCount: true })
      );

    this.computerName$ = this.computerService.getComputerName();
  }

  get paginatedProcesses$(): Observable<ProcessInfo[]> {
    return this.processes$.pipe(
      map(processes => {
        const start = (this.currentPage - 1) * this.pageSize;
        return processes.slice(start, start + this.pageSize);
      })
    );
  }

  get totalPages$(): Observable<number> {
    return this.processes$.pipe(
      map(processes => Math.ceil(processes.length / this.pageSize))
    );
  }

  private knownSystemNames: string[] = [ "Idle", "System", "csrss", "smss", "winlogon", "services", "lsass", "svchost", "fontdrvhost", "dwm" ];
}
