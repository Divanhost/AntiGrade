import { ExamType } from './exam-type.model';
import { Employee } from './employee.model';

export class SubjectDto {
    id: number;
    name: string;
    examType: ExamType;
    teachers: Employee[];
    mainTeacher: Employee;
}
