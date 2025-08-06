import { Component, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { ProcessInfo } from '../../models/ProcessInfo';
import { ProcessMenuComponent } from '../process-menu/process-menu.component';
import { ProcessPriority } from '../../models/ProcessPriority';

@Component({
  selector: 'app-process-card',
  standalone: true,
  imports: [MatCardModule, MatChipsModule, ProcessMenuComponent ],
  templateUrl: './process-card.component.html',
  styleUrl: './process-card.component.css'
})
export class ProcessCardComponent {
  @Input() process!: ProcessInfo;
  public priorityEnum = ProcessPriority
}
