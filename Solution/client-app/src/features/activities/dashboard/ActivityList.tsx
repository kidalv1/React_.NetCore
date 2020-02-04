import React from 'react'
import { Item, Image, Button, Label, Segment } from 'semantic-ui-react'
import { IActivity } from '../../../app/models/activity'

interface IProps {
    activities: IActivity[]
    selectActivity: (id: string) => void;
    deleteActivity : (id: string) => void;
}
const ActivityList: React.FC<IProps> = ({ activities, selectActivity ,deleteActivity }) => {
    return (
        <Segment clearing>
            <Item.Group divided>
                {activities.map(item => (
                    <Item key={item.id}>
                        <Item.Content>
                            <Item.Header as='a'>{item.title}</Item.Header>
                            <Item.Meta>{item.date}</Item.Meta>
                            <div>{item.description}</div>
                            <div>{item.city}</div>
                            <div>{item.vanue}</div>
                            <Item.Description>
                            </Item.Description>
                            <Item.Extra>
                                <Button onClick={() => selectActivity(item.id)} floated="right" content="View" color="blue" />
                                <Button onClick={() => deleteActivity(item.id)} floated="right" content="Delete" color="red" />
                                <Label >{item.category}</Label>
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}


            </Item.Group>
        </Segment>


    )
}

export default ActivityList;
