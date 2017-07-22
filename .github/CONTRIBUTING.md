### Contributing to Virtual Universe

**The Virtual Universe project is an open source project.  We welcome and encourage those who
 want to contribute code to do so.  By working together we all can bring a virtual planetary grid
 that encourages improvement of the future of virtual worlds.**
 
 ### How to contribute code
 **Contributing code to Virtual Universe is actually really easy to do.  Just follow these steps and we will
 take it from there.

1. Fork a copy of one of the following repositories to your github account:

1-A For contributing to the stable version of the architecture use Virtual-Universe/Virtual-Universe

1-B For contributing to the development version of the architecture use Virtual-Universe/Virtual-Dev

2. If you are contributing code which:

2-A. Requires the addition of a new .cs file please use the file in BuildingTools/EmptyClass.cs
 then move to next step.

2-B. Adds, Edits, or Removes code from a current .cs file move to next step.

3. Test your code locally to make sure you have no errors both in your build and in your operation.

Note: Be sure your code does not contain Windows line endings.  The coding standard is Unix line endings.
  The easiest way to obtain this if you use Visual Studios is to get Strip'em here: http://www.grebulon.com/software/stripem2010.zip
 Install this and the rest will be taken care of for you when you save your .cs files.

4. When you have completed steps described in number 2 and 3 then you must do the following:

4-A. Commit your changes in your local repository.

4-B. Push your commits to your repository on github.

4-C. Create an issue at the appropriate repository you are contributing to describing your request:

4-C-1. For pull requests being submitted to the stable architecture open issue at https://github.com/Virtual-Universe/Virtual-Universe/issues

4-C-2. For pull requests being submitted to the development architecture open issue at https://github.com/Virtual-Universe/Virtual-Dev/issues

4-D. Create a pull request.  Make sure to click the "compare across forks" link.  The forks must be as follows:

For the stable architecture repository:

 base fork Virtual-Universe/Virtual-Universe base master
 head fork <user>/Virtual-Universe compare master

For the development architecture repository:

 base fork Virtual-Universe/Virtual-Dev base master
 head fork <user>/Virtual-Dev compare master

Then click the Create Pull Request button when your commit shows up and press the green button to submit the request.
  Be sure to reference your issue number in your pull request.

5. After submitting the pull request you will see a 2 step test process begin. Wait for the process to complete.
  Don't panic this could take up to 15 minutes to complete.  Our repos will test build all pull requests.
  If after the tests complete you see 2 green checkmarks for both appveyor and travis-ci then no need for you to worry.
  If you see red check marks your pull request will be rejected and closed by the development team and you will
 have to resubmit it.  If you have successfully completed step 3 then the checkmarks should return green with success messages.

6. If step 5 succeeds then all you need to do is wait to see if your pull request is accepted into the Virtual-Universe codebase.
  Your request will be reviewed by the development team including the developer responsible for development of the area your
 pull request covers.  If 3 of the team members agree with your commit then your commit will be accepted and merged.  If your
 request is rejected we will let you know the reason when we close the pull request.  Usually the reason will be posted in
 the issue you created.

Thats all there is to it.  If you successfully contribute code and your name is not in our CONTRIBUTORS.txt then don't worry it
 will get added.

We hope you enjoy Virtual-Universe and all the great things we are working on to make our new virtual planetary grid architecture 
 even better.**
 
 ### Legal Notice
 
 **The Virtual Universe Open Source Project the name Virtual-planets, Virtual-Universe, Second Galaxy, Galaxy Futures, 
 the GalaxyGrid virtual world architecture, GalaxyScript, GalaxyPhysX are the property of the Second Galaxy Development Team 
 ALL RIGHTS RESERVED!

 For more information please see Documentation/Licenses/VirtualUniverseLicense for more information.

 The Name WhiteCore, WhiteCore-Sim, Aurora-Sim, Opensimulator, OpenSim are the property of their respective developers and is obtained
 by the Virtual Universe project under the open source licenses provided by the respective project.  You can find their licenses in
 the Licenses directory.** 
 
 **The Second Galaxy Development Team**