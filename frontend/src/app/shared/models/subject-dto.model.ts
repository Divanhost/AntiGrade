import { ExamType } from './exam-type.model';
import { Employee } from './employee.model';

export class SubjectDto {
    id: number;
    name: string;
    examTypeId: number;
    examType: ExamType;
    teachers: Employee[];
    mainTeacher: Employee;
}
