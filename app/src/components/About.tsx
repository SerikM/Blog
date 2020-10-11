import * as React from 'react';
import { connect } from 'react-redux';

const About = () => (



    <div>
        <h1> About me </h1>
        My name is SerikM. I am a developer. I build things. I do not know if I am any good but I have been doing this <br />
        for a long time and can hardly do anything else in life. Ah and I also built a few sites including this one you're browsing.
  </div>
);

export default connect()(About);
