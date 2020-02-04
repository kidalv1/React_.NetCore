import React, { useState, FormEvent } from 'react'
import { Segment, Form, Button } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity';
import {v4 as uuid} from 'uuid'
interface IProps {
    setEditMode: (editMode: boolean) => void
    activity: IActivity
    createActivity: (activity: IActivity) => void
    editActivity: (activity: IActivity) => void
}
const ActivityForm: React.FC<IProps> = ({ setEditMode, activity: initFormState, createActivity, editActivity }) => {
    const initForm = () => {
        if (initFormState) {
            return initFormState
        } else {
            return {
                id: '',
                title: '',
                category: '',
                description: '',
                date: '',
                city: '',
                vanue: ''
            };
        }
    }
    const [activity, setActivity] = useState<IActivity>(initForm);
    const handleSubmit = () => {
        if (activity.id.length === 0) {
            let newActivity = {
                ...activity,
                id: 'guid'
            }
            createActivity(activity);

        }
        else {
            editActivity(activity);
        }
    }
    const handleInputChange = (event: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = event.currentTarget;
        setActivity({ ...activity, [name]: value })
    }
    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit}>
                <Form.Input onChange={handleInputChange} name="title" placeholder='Title' value={activity.title} />
                <Form.TextArea onChange={handleInputChange} name="description" row={2} placeholder='Description' value={activity.description} />
                <Form.Input onChange={handleInputChange} name="category" placeholder='Category' value={activity.category} />
                <Form.Input onChange={handleInputChange} name="date" type="datetime-local" placeholder='Date ' value={activity.date} />
                <Form.Input onChange={handleInputChange} name="city" placeholder='City' value={activity.city} />
                <Form.Input onChange={handleInputChange} name="vanue" placeholder='Vanue' value={activity.vanue} />
                <Button floated="right" positive type="submit" content="submit" />
                <Button onClick={() => setEditMode(false)} floated="right" positive type="submit" content="Cancel" />

            </Form>
        </Segment>
    )
}

export default ActivityForm;
