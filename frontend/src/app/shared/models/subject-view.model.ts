import { ExamType } from './exam-type.model';
import { Group } from './group.model';

export class SubjectView {
    id: number;
    name: string;
    examType: ExamType;
    group: Group;
    // hasPlan: boolean;
}
