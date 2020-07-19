import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as ApplicationStore from '../store/Authentication';

// At runtime, Redux will merge together...
type ApplicationProps =
    ApplicationStore.AuthenticationState // ... state we've requested from the Redux store
    & typeof ApplicationStore.actionCreators; // ... plus action creators we've requested

class LoginPage extends React.PureComponent<ApplicationProps> {
    public state = {
        username: '',
        password: '',
        submitted: false
    };
    // This method is called when the component is first added to the document
    public componentDidMount() {
    }

    // This method is called when the route parameters change
    public componentDidUpdate() {
    }

    handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        const { name, value } = e.currentTarget;
        this.setState({ [name]: value });
    }

    handleLogin = () => {
        const { username, password } = this.state;
        if (username && password) {
            this.props.authenticate(username, password);
        }
    }

    handleLogout = () => {
        this.props.logout();
    }

    public render() {
        const { isLoggedIn } = this.props;
        const { username, password, submitted } = this.state;
        return (
            <div className="col-md-6 col-md-offset-3">
                <h2>Login</h2>
                {
                    isLoggedIn
                        ? <button className="btn btn-primary" onClick={this.handleLogout}>Logout</button>
                        : <div>
                              <div className={'form-group' + (submitted && !username ? ' has-error' : '')}>
                                  <label htmlFor="username">Username</label>
                                  <input type="text" className="form-control" name="username" value={username} onChange={this.handleChange}/>
                                  {submitted &&
                                      !username &&
                                      <div className="help-block">Username is required</div>
                                  }
                              </div>
                              <div className={'form-group' + (submitted && !password ? ' has-error' : '')}>
                                  <label htmlFor="password">Password</label>
                                  <input type="password" className="form-control" name="password" value={password} onChange={this.handleChange}/>
                                  {submitted &&
                                      !password &&
                                      <div className="help-block">Password is required</div>
                                  }
                              </div>
                              <div className="form-group">
                                <button className="btn btn-primary" onClick={this.handleLogin}>Login</button>
                              </div>
                          </div>
                }
            </div>
        );
    }
}

export default connect(
    (state: ApplicationState) => state.authentication, // Selects which state properties are merged into the component's props
    ApplicationStore.actionCreators // Selects which action creators are merged into the component's props
)(LoginPage as any);