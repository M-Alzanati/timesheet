import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { SubmitSheetComponent } from './submit-sheet.component';

describe('SubmitSheetComponent', () => {
    let component: SubmitSheetComponent;
    let fixture: ComponentFixture<SubmitSheetComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [SubmitSheetComponent]
        }).compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(SubmitSheetComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
