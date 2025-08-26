import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcessDetailsModalComponent } from './process-details-modal.component';

describe('ProcessDetailsModalComponent', () => {
  let component: ProcessDetailsModalComponent;
  let fixture: ComponentFixture<ProcessDetailsModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProcessDetailsModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProcessDetailsModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
