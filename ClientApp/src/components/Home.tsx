import * as React from 'react';
import { connect } from 'react-redux';

const Home = () => (
  <div>
    <h1>Hello, world!</h1>
    <p>Welcome to my site. There is not much you can do here atm but, it's going to look amazing in a little while.</p>
    </div>
);

export default connect()(Home);
