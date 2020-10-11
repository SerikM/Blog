import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';

type ProjectsProps = RouteComponentProps<{}>;

class Projects extends React.PureComponent<ProjectsProps> {
    public render() {
        return (
            <React.Fragment>
              <h1>My Projects</h1>

            </React.Fragment>
        );
    }
};

export default connect()(Projects);
